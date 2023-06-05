import { MenuItem } from './menu-item';

export const MENU: MenuItem[] = [
  { label: 'Books', routerLink: ['/books'], permissions: ['read:books'] },
  { label: 'Add User', routerLink: ['/users/create'], permissions: ['write:users'] },
  { label: 'User', routerLink: ['/users'], permissions: ['read:users'] },
  { label: 'Reservations', routerLink: ['/reservations'], permissions: ['read:users'] },
];
