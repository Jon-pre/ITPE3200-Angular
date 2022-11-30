import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Konto } from '../Konto';



@Component({
    templateUrl: './endre-component.html'
})
export class EndreComponent {
endreSkjema: FormGroup;
    validering = {
        id: [""],
        kontonavn: [
            null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ],
        land: [
            null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
        ]
    }
   
    constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, private fb: FormBuilder) {
        this.endreSkjema = fb.group(this.validering);
       

    }



    ngOnInit() {
        this.route.params.subscribe(params => {
            this.endreSkjema.patchValue({ id: Number(params.id) });
        })
    }

    vedSubmit() {
        this.endreEnKonto();
    }

    endreKonto(id: number) {
        this.http.get<Konto>("api/konto/" + id)
            .subscribe(
                konto => {
                    this.endreSkjema.patchValue({ id: konto.id });
                    this.endreSkjema.patchValue({ kontonavn: konto.kontonavn });
                    this.endreSkjema.patchValue({ land: konto.land });
                },
                error => console.log(error)
            );
    }
   
    endreEnKonto() {
        const endretKonto = new Konto();
        endretKonto.id = this.endreSkjema.value.id;
        endretKonto.kontonavn = this.endreSkjema.value.kontonavn;
        endretKonto.land = this.endreSkjema.value.land;

        this.http.put("api/konto/", endretKonto).subscribe(retur => {
            this.router.navigate(['/', 'konto']);
        },
            error => console.log(error)
        );
    }
}
