import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core'
import { Router } from '@angular/router';
import { Aksje } from 'app/Aksje'

@Component({
    templateUrl: './home-component.html'
})
export class HomeComponent {
    alleAksjer: Array<Aksje>;
    laster: boolean;

    constructor(private http: HttpClient, private router: Router) { }

    ngOnInit() {
        this.laster = true;
        this.hentAlleAksjer();
    }

    hentAlleAksjer() {
        this.http.get<Aksje[]>("api/aksje/").subscribe(aksjene => {
            this.alleAksjer = aksjene;
            this.laster = false;
        },
            error => console.log(error)
        );
    };
    
    slettEnAksje(id: number) {
        this.http.delete("api/aksje/"+id)
            .subscribe(retur => {
                this.hentAlleAksjer();
                this.router.navigate(['/home']);
            },
            error => console.log(error)
            );
    };
    
    
}