import { Injectable } from "@angular/core";
import { Notify } from "notiflix";

export interface Notification {
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor() {

  }

  success(model: Notification | string) {
    if (typeof model == 'string') {
      Notify.success(model);
    }
    else {
      Notify.success(model.message);
    }
  }

  failure(model: Notification) {
    Notify.failure(model.message);
  }
}
