import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Konto } from '../Konto';

@Component({
    templateUrl: './konto-component.html'
})
export class KontoComponent {
    DinKonto: Array<Konto>;
    laster: boolean;

    constructor(private http: HttpClient, private router: Router) { }

    ngOnInit(){
        this.laster = true;
        this.hentKontoer();
    }


    hentKontoer() {
        this.http.get<Konto[]>("api/konto/").subscribe(kontoene => {
            this.DinKonto = kontoene;
            this.laster = false;
        },
            error => console.log(error)
        );
    };

    
}