import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Kunde } from "./Kunde";


@Component({
    selector: "app-root",
    templateUrl: "SPA.html"
})
export class SPA {

    visSkjemaRegistrere: boolean;
    visListe: boolean;
    alleKunder: Array<Kunde>;
    skjema: FormGroup;
    laster: boolean;

    validering = {
        id: [""],
        fornavn: [
            null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],
        etternavn: [
            null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],
        adresse: [
            null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],
        postnr: [
            null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])
        ],
        poststed: [
            null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ]
    }

    constructor(private _http: HttpClient, private fb: FormBuilder) {
        this.skjema = fb.group(this.validering);
    }

    ngOnInit() {
        this.laster = true;
        this.hentAlleKunder();
        this.visListe = true;
    }

    hentAlleKunder() {
        this._http.get<Kunde[]>("api/kunde/")
            .subscribe(kundene => {
                this.alleKunder = kundene;
                this.laster = false;
            },
                error => console.log(error),
                () => console.log("ferdig get-api/kunde")
            );
    };

    vedSubmit() {
        if (this.visSkjemaRegistrere) {
            this.lagreKunde();
        }
        else {
            this.endreEnKunde();
        }
    }

    registrerKunde() {
        // må resette verdiene i skjema dersom skjema har blitt brukt til endringer
        this.skjema.setValue({
            id: "",
            fornavn: "",
            etternavn: "",
            adresse: "",
            postnr: "",
            poststed: ""
        });
        this.skjema.markAsPristine();
        this.visListe = false;
        this.visSkjemaRegistrere = true;
    }

    tilbakeTilListe() {
        this.visListe = true;
    }

    lagreKunde() {
        const lagretKunde = new Kunde();

        lagretKunde.fornavn = this.skjema.value.fornavn;
        lagretKunde.etternavn = this.skjema.value.etternavn;
        lagretKunde.adresse = this.skjema.value.adresse;
        lagretKunde.postnr = this.skjema.value.postnr;
        lagretKunde.poststed = this.skjema.value.poststed;

        this._http.post("api/kunde", lagretKunde)
            .subscribe(retur => {
                this.hentAlleKunder();
                this.visSkjemaRegistrere = false;
                this.visListe = true;
            },
                error => console.log(error)
            );
    };

    sletteKunde(id: number) {
        this._http.delete("api/kunde/" + id)
            .subscribe(retur => {
                this.hentAlleKunder();
            },
                error => console.log(error)
            );
    };

    endreKunde(id: number) {
        this._http.get<Kunde>("api/kunde/" + id)
            .subscribe(
                kunde => {
                    this.skjema.patchValue({ id: kunde.id });
                    this.skjema.patchValue({ fornavn: kunde.fornavn });
                    this.skjema.patchValue({ etternavn: kunde.etternavn });
                    this.skjema.patchValue({ adresse: kunde.adresse });
                    this.skjema.patchValue({ postnr: kunde.postnr });
                    this.skjema.patchValue({ poststed: kunde.poststed });
                },
                error => console.log(error)
            );
        this.visSkjemaRegistrere = false;
        this.visListe = false;
    }

    endreEnKunde() {
        const endretKunde = new Kunde();
        endretKunde.id = this.skjema.value.id;
        endretKunde.fornavn = this.skjema.value.fornavn;
        endretKunde.etternavn = this.skjema.value.etternavn;
        endretKunde.adresse = this.skjema.value.adresse;
        endretKunde.postnr = this.skjema.value.postnr;
        endretKunde.poststed = this.skjema.value.poststed;

        this._http.put("api/kunde/", endretKunde)
            .subscribe(
                retur => {
                    this.hentAlleKunder();
                    this.visListe = true;
                },
                error => console.log(error)
            );
    }
}

