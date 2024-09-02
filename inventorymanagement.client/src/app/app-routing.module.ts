import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from '../products/products.component';
import { ChartsComponent } from './charts/charts.component';

const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: 'charts', component: ChartsComponent },
  { path: '', redirectTo: '/products', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
