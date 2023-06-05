import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TruncatePipe } from './pipes/truncate.pipe';
import { BookCardComponent } from './components/book-card/book-card.component';
import { RouterModule } from '@angular/router';

const DECLARATIONS = [
  BookCardComponent,
  TruncatePipe
]


@NgModule({
  declarations: [
    ...DECLARATIONS
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
