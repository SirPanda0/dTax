import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class HttpService {

  api = 'http://localhost:5000/api/';
  private headers = new HttpHeaders({
    'Content-Type': 'application/json;charset=utf-8',
  });
  private httpOptions = ({ headers: this.headers, withCredentials: true });
  constructor(private http: HttpClient) { }

  post(link, value) {
   return this.http.post(this.api + link, value, this.httpOptions ).pipe(response => response);
  }
  get(apig){
    return this.http
    .get(this.api + apig, this.httpOptions );
  
}

}
