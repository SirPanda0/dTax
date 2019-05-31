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
  get(apig) {
    return this.http
    .get(this.api + apig, this.httpOptions );
}

makeFileRequest(api: string, files: Array<File>) {
  return new Promise((resolve, reject) => {
      const formData: any = new FormData();
      let xhr = new XMLHttpRequest();
      formData.append('uploadedFile', files[0]);
      xhr.onreadystatechange = function () {
          if (xhr.readyState === 4) {
              if (xhr.status === 200) {
                  resolve(JSON.parse(xhr.response));
              } else {
                  reject(xhr.response);
              }
          }
      };

      xhr.open('POST', this.api + api, true);
      xhr.withCredentials = true;
      xhr.send(formData);
  });
}

}
