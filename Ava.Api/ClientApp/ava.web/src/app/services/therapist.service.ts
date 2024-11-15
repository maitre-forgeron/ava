import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Therapist } from '../models/therapist.model';

@Injectable({
  providedIn: 'root'
})
export class TherapistService {

  private apiUrl = 'http://localhost:5221/api/therapist/alltherapists';

  constructor(private http: HttpClient) { }

  getAllTherapists(): Observable<Therapist[]> {
    return this.http.get<Therapist[]>(this.apiUrl);
  }
}
