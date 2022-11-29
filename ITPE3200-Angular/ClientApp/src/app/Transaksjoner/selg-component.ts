import { HttpClient } from "@angular/common/http";
import { Component, ElementRef, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Aksje } from "../Aksje";
import { Konto } from "../Konto";

@Component({
    templateUrl: './selg-component.html'
})
export class SelgComponent {
    skjema: FormGroup;
    alleAksjer: Array<Aksje>;
    aksjeFiltered: Array<Aksje>;
    @ViewChild("navn", null) navn: ElementRef;
    @ViewChild("prosent", null) prosent: ElementRef;
    @ViewChild("pris", null) pris: ElementRef;
    @ViewChild("kontonavn", null) kontonavn: ElementRef;
    @ViewChild("land", null) land: ElementRef;
    @ViewChild("kontobalanse", null) kontobalanse: ElementRef;


    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
     
    }


    ngOnInit() {       
        this.route.params.subscribe(params => {
            var kontoId: number = 1;
            this.hentEnKonto(kontoId);
            this.hentEn(params.id);
            console.log(params.id);
        });
    }
    
    hentEn(id: number) {
        this.http.get<Aksje>("api/aksje/" + id).subscribe(aksje => {
            this.navn.nativeElement.innerHTML = aksje.navn;
            this.prosent.nativeElement.innerHTML = aksje.prosent;
            this.pris.nativeElement.innerHTML = aksje.pris;
        },
            error => console.log(error)
        );
    };

    hentEnKonto(id: number) {
        this.http.get<Konto>("api/konto/" + id).subscribe(konto => {
            this.kontonavn.nativeElement.innerHTML = konto.kontonavn;
            this.land.nativeElement.innerHTML = konto.land;
            this.kontobalanse.nativeElement.innerHTML = konto.kontobalanse;
        },
            error => console.log(error)
        );
    }
    selg() {
        var kontoId: number = 1;
        console.log(kontoId);
        var kontoNavn = this.kontonavn.nativeElement.innerHTML
        console.log(kontoNavn);
        var aksjeSum: number = this.pris.nativeElement.innerHTML;
        console.log(aksjeSum);
        var kontoSum: number = this.kontobalanse.nativeElement.innerHTML;
        console.log(kontoSum);
        var land = this.land.nativeElement.innerHTML;

        var sum: number = Number(kontoSum) + Number(aksjeSum);
        console.log(sum);
        const selgAksje = new Konto();
        selgAksje.id = kontoId;
        selgAksje.kontonavn = kontoNavn;
        selgAksje.land = land;
        selgAksje.kontobalanse = sum;
        if (sum < 1000000) {
            this.http.post<Konto>("api/aksje", selgAksje).subscribe(retur =>
                this.router.navigate(['/konto']))
            window.location.reload();
        } else {
            alert("Du har nådd dagens øvre grense, kom tilbake i morgen");
        }
    }
}