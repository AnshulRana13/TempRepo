import { Component, OnInit } from '@angular/core';
import {PhotosService} from '../services/photos.service';
import {Photo} from '../models/photos/photo';
@Component({
  selector: 'app-photos',
  templateUrl: './photos.component.html',
  styleUrls: ['./photos.component.css']
})
export class PhotosComponent implements OnInit {

  photos: Photo[] =[];
  isLoaded :boolean ;
  constructor(private photoService : PhotosService) { }

  ngOnInit(): void {

    this.photoService.getPhotos().subscribe((data: Photo[])=>{
            this.photos = data;
            this.isLoaded = true;
            console.log(this.photos);
    })
  }

}
