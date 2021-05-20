import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { User } from '../models/users/user';
import { UsersService } from '../services/users.service';
@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
   id : string
   user: User
  constructor(private route: ActivatedRoute,private userService : UsersService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')

    this.userService.getUser(this.id).subscribe((data:any)=>{
      this.user = data;
      console.log(data);
    });
  }

}
