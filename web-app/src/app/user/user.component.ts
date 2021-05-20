import { Component, OnInit } from '@angular/core';
import {UsersService} from '../services/users.service';
import {User} from '../models/users/user';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
 //users: any;
  users: User[] =[];
  
  constructor(private userService : UsersService,private router: Router) {
    
   }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((data: User[])=> {
      this.users = data
      console.log(this.users);
    });
  }

}
