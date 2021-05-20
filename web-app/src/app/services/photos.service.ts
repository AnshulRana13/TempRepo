import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Photo } from '../models/photos/photo';
import { map } from 'rxjs/operators';
import {AppConstant} from '../models/common/app.constant'
@Injectable({
    providedIn: 'root',
})
export class PhotosService {
    configUrl = AppConstant.BASE_URL + '/api/v1/photos';
  //configUrl ="https://jsonplaceholder.typicode.com/photos";
  constructor(private http: HttpClient) { 
  }

  getPhotos() {
    return this.http.get(this.configUrl);
  }

}