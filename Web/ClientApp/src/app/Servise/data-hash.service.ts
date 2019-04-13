import { Injectable } from '@angular/core';
import { Base64 } from 'js-base64';

@Injectable()
export class DataHashService {

  constructor() {}


  public EncodeJSON(data): string {
    return Base64.encode(JSON.stringify(data));
  }

  public DecodeJSON(data): any {
    return data ? JSON.parse(Base64.decode(data)) : '';
  }
}
