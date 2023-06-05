import { Pipe, PipeTransform } from '@angular/core';
import { AuthService } from 'projects/core/src/lib/services/auth.service';

@Pipe({
  name: 'hasPermissions'
})
export class HasPermissionsPipe implements PipeTransform {

  constructor(private auth: AuthService) {

  }

  transform(value: string[]): unknown {
    return this.auth.hasPermissions(value);
  }

}
