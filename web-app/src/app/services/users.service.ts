import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of,BehaviorSubject } from 'rxjs';
import { User } from '../models/users/user';
import { map } from 'rxjs/operators';
import {AppConstant} from '../models/common/app.constant'

@Injectable({
    providedIn: 'root',
})
export class UsersService {

  private users = new Observable<User[]>();

  
  configUrl = AppConstant.BASE_URL + '/api/v1/users';
  constructor(private http: HttpClient) { 
  }

  getUsers() {
    return this.http.get(this.configUrl);
  }
  

  getUser(id: number | string) {
    return this.getUsers().pipe(
      // (+) before `id` turns the string into a number
      map((users: User[]) => users.find(user => user.id === +id))
    );
  }
}