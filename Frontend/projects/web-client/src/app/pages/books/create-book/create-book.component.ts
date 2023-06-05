import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Author } from 'projects/core/src/lib/interfaces/author';
import { PagedList } from 'projects/core/src/lib/interfaces/paged-list';
import { AuthorService } from 'projects/core/src/lib/services/author.service';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.scss']
})
export class CreateBookComponent {

  pagedAuthors: PagedList<Author> = {} as PagedList<Author>;

  constructor(private fb: FormBuilder, private bookService: BookService, private router: Router, private authorService: AuthorService, private notificationService: NotificationService) {

  }

  form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    authorId: [null, Validators.required]
  });

  ngOnInit() {
    this.getAuthors();
  }

  getAuthors() {
    this.authorService.getAuthors()
      .subscribe(data => {
        this.pagedAuthors = data;
      });
  }

  createBook() {
    if (this.form.valid) {
      this.bookService.createBook(this.form.value)
        .subscribe(data => {
          this.notificationService.success('Created Successfully');
          this.router.navigate(['../']);
        });
    }
  }
}
