import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Customer } from 'projects/core/src/lib/interfaces/customer';
import { PagedList } from 'projects/core/src/lib/interfaces/paged-list';
import { UserService } from 'projects/core/src/lib/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {

  pagedCustomers: PagedList<Customer> = {} as PagedList<Customer>;

  constructor(private route: ActivatedRoute){}

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.pagedCustomers = data['customers'];
    })
  }
}
