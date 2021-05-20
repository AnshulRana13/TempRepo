import { Component, OnInit } from '@angular/core';
import {AlbumsService} from '../services/albums.service';
import {Album} from '../models/albums/album';
@Component({
  selector: 'app-albums',
  templateUrl: './albums.component.html',
  styleUrls: ['./albums.component.css']
})
export class AlbumsComponent implements OnInit {

  albums : Album[] =[];
  constructor(private albumService : AlbumsService) { }

  ngOnInit(): void {

    this.albumService.getAlbums().subscribe((data : Album[])=>{
       this.albums = data;
       console.log(this.albums);
    });
  }

}
