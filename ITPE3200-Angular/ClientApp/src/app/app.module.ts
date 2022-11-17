/*import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
*/
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SPA } from './spa';

@NgModule({
 /* declarations: [
    AppComponent,
 
  ], */
    
  imports: [
    /*BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
    
    ]) */

      BrowserModule,
      ReactiveFormsModule,
      HttpClientModule
    
  ],
  declarations: [SPA],
    bootstrap: [SPA]
})
export class AppModule { }
