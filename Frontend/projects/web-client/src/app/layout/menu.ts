import { UserPermissions } from 'projects/core/src/lib/enums/permissions';
import { MenuItem } from './menu-item';

export const MENU: MenuItem[] = [
  { label: 'Books', routerLink: ['/books'], permissions: [UserPermissions.readBooks] },
  { label: 'Users', routerLink: ['/users'], permissions: [UserPermissions.readUsers] },
  { label: 'Reservations', routerLink: ['/reservations'], permissions: [UserPermissions.readUsers, UserPermissions.readBooks] },
];
