import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Photo } from '../models/photos/photo';
import { map } from 'rxjs/operators';
import {AppConstant} from '../models/common/app.constant'

@Injectable({
    providedIn: 'root',
})
export class AlbumsService {
  configUrl = AppConstant.BASE_URL + '/api/v1/albums';
 // configUrl ="https://jsonplaceholder.typicode.com/albums";
  constructor(private http: HttpClient) { 
  }

  getAlbums() {
    return this.http.get(this.configUrl);
  }

}