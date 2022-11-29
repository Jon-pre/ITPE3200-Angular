import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core'
import { Router, ActivatedRoute } from '@angular/router';
import { Aksje } from '../Aksje';
import { Konto } from '../Konto';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms'

@Component({
    templateUrl: './kjop-component.html'
})
export class KjopComponent {
    skjema: FormGroup;
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
            this.hentEn(params.id);
         //   this.hentkonto(params.id);
            console.log(params.id);
            this.hentEnKonto(kontoId);
            console.log(kontoId);
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
    kjop() {
        var kontoId: number = 1;
        console.log(kontoId);
        var kontoNavn = this.kontonavn.nativeElement.innerHTML
        console.log(kontoNavn);
        var aksjeSum: number = this.pris.nativeElement.innerHTML;
        console.log(aksjeSum);
        var kontoSum: number = this.kontobalanse.nativeElement.innerHTML;
        console.log(kontoId);
        var land = this.land.nativeElement.innerHTML;

        var sum: number = kontoSum - aksjeSum;
        const kjopAksje = new Konto();
        kjopAksje.id = kontoId;
        kjopAksje.kontonavn = kontoNavn;
        kjopAksje.land = land;
        kjopAksje.kontobalanse = sum;
        if (sum >= 0) {
            this.http.post<Konto>("api/aksje", kjopAksje).subscribe(retur =>
                this.router.navigate(['/konto']))
            window.location.reload();
        } else {
            console.log("Kontobalanse er for lav");
            alert("Kontobalanse er for lav for å fullføre kjøp");
        }
    }
}