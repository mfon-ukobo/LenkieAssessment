import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TruncatePipe } from './pipes/truncate.pipe';
import { BookCardComponent } from './components/book-card/book-card.component';
import { RouterModule } from '@angular/router';
import { HasPermissionsPipe } from './pipes/has-permissions.pipe';
import { CreateAuthorComponent } from './components/create-author/create-author.component';

const DECLARATIONS = [
  BookCardComponent,
  TruncatePipe,
  HasPermissionsPipe
]


@NgModule({
  declarations: [
    ...DECLARATIONS,
    CreateAuthorComponent,
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
