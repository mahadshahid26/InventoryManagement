import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { ProductsComponent } from '../products/products.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module'; // Import routing module
import { FormsModule } from '@angular/forms';
import { ChartsComponent } from './charts/charts.component'; // Import FormsModule

const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: '', redirectTo: '/app-root', pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    ChartsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    NgbModule,
    FormsModule
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
