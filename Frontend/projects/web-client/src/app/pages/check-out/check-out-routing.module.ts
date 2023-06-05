import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckOutComponent } from './check-out.component';
import { BookResolver } from '../../resolvers/book.resolver';

const routes: Routes = [
  { path: '', component: CheckOutComponent, resolve: {book: BookResolver}, runGuardsAndResolvers: 'paramsChange' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CheckOutRoutingModule {}
