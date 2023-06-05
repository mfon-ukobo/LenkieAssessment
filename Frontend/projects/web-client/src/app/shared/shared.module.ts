import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TruncatePipe } from './pipes/truncate.pipe';
import { BookCardComponent } from './components/book-card/book-card.component';
import { RouterModule } from '@angular/router';
import { HasPermissionsPipe } from './pipes/has-permissions.pipe';

const DECLARATIONS = [
  BookCardComponent,
  TruncatePipe,
  HasPermissionsPipe
]


@NgModule({
  declarations: [
    ...DECLARATIONS,
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    ...DECLARATIONS
  ]
})
export class SharedModule { }
