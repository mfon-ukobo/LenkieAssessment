import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Author } from 'projects/core/src/lib/interfaces/author';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { PagedList } from 'projects/core/src/lib/interfaces/paged-list';
import { AuthorService } from 'projects/core/src/lib/services/author.service';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.scss']
})
export class EditBookComponent {
  pagedAuthors: PagedList<Author> = {} as PagedList<Author>;
  book: Book = {} as Book;

  constructor(private fb: FormBuilder, private bookService: BookService, private router: Router, private authorService: AuthorService, private route: ActivatedRoute, private notificationService: NotificationService) {

  }

  form = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    authorId: [null, Validators.required]
  });

  ngOnInit() {
    this.getAuthors();
    this.route.data.subscribe((data) => {
      this.book = data['book'];
      this.form.patchValue(this.book);
    });
  }

  getAuthors() {
    this.authorService.getAuthors()
      .subscribe(data => {
        this.pagedAuthors = data;
      });
  }

  editBook() {
    if (this.form.valid) {
      this.bookService.updateBook(this.book.id, this.form.value)
        .subscribe(data => {
          this.notificationService.success('Update Successful');
        });
    }
  }
}
