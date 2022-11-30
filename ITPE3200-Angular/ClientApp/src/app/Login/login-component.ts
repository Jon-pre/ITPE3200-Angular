import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { FormControl, FormGroup, Validators, FormBuilder } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Konto } from "../Konto";

@Component({
    templateUrl: './login-component.html'
})
export class LoginComponent {
    loginSkjema: FormGroup;


    validering = {
        id: [""],
        brukernavn: [
            null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],

        passord: [
            null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{4,}")])
        ],
    }

    constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
        this.loginSkjema = fb.group(this.validering)
    }



    vedSubmit() {
        this.loginAksje();
    }


        loginAksje() {

            const kontocred = new Konto();
            kontocred.brukernavn = this.loginSkjema.value.brukernavn;
            kontocred.passord = this.loginSkjema.value.passord;

            this.http.post<Konto>("api/konto", kontocred).subscribe(retur => {
                this.router.navigate(['/konto']);
            },
                
                error => console.log(error)
            );
    }

        hentId() {
            const kontocred = new Konto();
            kontocred.passord = this.loginSkjema.value.passord;

            this.http.get<Konto>("api/konto").subscribe(retur => {

            })
    }


    
}


