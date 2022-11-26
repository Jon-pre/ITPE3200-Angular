import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    templateUrl: './login-component.html'
})
export class LoginComponent {

    loginSkjema: FormGroup;

    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {
        this.loginSkjema = new FormGroup({
            brukernavn: new FormControl(),
            passord: new FormControl()
        });
    }


   // login()

}