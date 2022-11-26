import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './Navigation/navigation-component'
import { AppRoutingModule } from './app-routing.module';
import { KontoComponent } from './Konto/konto-component'
import { KjopComponent } from './Transaksjoner/kjop-component';
import { HomeComponent } from './Home/home-component';
import { EndreComponent } from './Konto/endre-component';
import { SelgComponent } from './Transaksjoner/selg-component';

@NgModule({
  declarations: [
        AppComponent,
        NavMenuComponent,
        KontoComponent,
        KjopComponent,
        SelgComponent,
        HomeComponent,
        EndreComponent
  ], 
    
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        FormsModule,
        HttpClientModule,
        AppRoutingModule,
        ReactiveFormsModule
       ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
