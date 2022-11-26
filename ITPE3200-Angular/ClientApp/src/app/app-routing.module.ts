import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { KjopComponent } from "./Transaksjoner/kjop-component";
import { HomeComponent } from "./Home/home-component";
import { EndreComponent } from "./Konto/endre-component";
import { KontoComponent } from "./Konto/konto-component";
import { SelgComponent } from "./Transaksjoner/selg-component";


const appRoots: Routes = [
    { path: 'konto',component: KontoComponent },
    { path: 'kjop/:id', component: KjopComponent },
    { path: 'selg/:id', component: SelgComponent},
    { path: 'home', component: HomeComponent },
    { path: 'endre/:id', component: EndreComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoots)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }