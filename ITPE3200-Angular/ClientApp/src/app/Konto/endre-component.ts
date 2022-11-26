import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Konto } from '../Konto';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';



@Component({
    templateUrl: './endre-component.html'
})

export class EndreComponent {
    skjema: FormGroup;
    /*
    constructor(private http: HttpClient, private fb: FormBuilder,
        private route: ActivatedRoute, private router: Router) {
        this.skjema = fb.group(this.validering);
    }
    */
    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, private fb: FormBuilder) {
        this.skjema = new FormGroup({
            id: new FormControl(),
            navn: new FormControl(),
            land: new FormControl()
        })
    }



    ngOnInit() {
        this.route.params.subscribe(params => {
            this.endreKonto(params.id);
        })
    }

    endreKonto(id: number) {
        this.http.get<Konto>("api/konto/" + id)
            .subscribe(
                konto => {
                    this.skjema.patchValue({ id: konto.id });
                    this.skjema.patchValue({ navn: konto.navn });
                    this.skjema.patchValue({ land: konto.land });
                },
                error => console.log(error)
            );
    }
    
    endreEnKonto() {
        const endretKonto = new Konto();
        endretKonto.id = this.skjema.value.id;
        endretKonto.navn = this.skjema.value.navn;
        endretKonto.land = this.skjema.value.land;

        this.http.put<Konto[]>("api/konto", endretKonto).subscribe(retur => {
            this.router.navigate(['/konto']);
        },
            error => console.log(error)
        );
    }
}
