import { HttpClient } from "@angular/common/http";
import { Component, ElementRef, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Aksje } from "../Aksje";

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

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
        /*this.skjema = new FormGroup({
            id: new FormControl(),
            navn: new FormControl(),
            prosent: new FormControl(),
            pris: new FormControl()
        });*/
    }


    ngOnInit() {
        /* this.hentAlleAksjer();
        /*this.id = this.route.snapshot.queryParamMap.get('id');*/
        this.route.params.subscribe(params => {
            this.hentEn(params.id);
            console.log(params.id);
        });
    }
    /*
    hentAlleAksjer() {
        this.http.get<Aksje[]>("api/aksje/").subscribe(aksjene => {
            this.alleAksjer = aksjene;
        },
            error => console.log(error)
        );
    };
    */
    hentEn(id: number) {
        this.http.get<Aksje>("api/aksje/" + id).subscribe(aksje => {
            /*
        this.skjema.patchValue({ id: aksje.id });
        this.skjema.patchValue({ navn: aksje.navn });
        this.skjema.patchValue({ prosent: aksje.prosent });
        this.skjema.patchValue({ pris: aksje.pris})*/
            this.navn.nativeElement.innerHTML = aksje.navn;
            this.prosent.nativeElement.innerHTML = aksje.prosent;
            this.pris.nativeElement.innerHTML = aksje.pris;
        },
            error => console.log(error)
        );
    };
}