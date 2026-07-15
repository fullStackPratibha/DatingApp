import { Component, signal ,inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { lastValueFrom } from 'rxjs/internal/lastValueFrom';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit  {
  private http = inject(HttpClient);
  protected title = 'Dating App';
  protected member = signal<any>([]);


  async ngOnInit() {
    this.member.set(await this.getMembers())
  }

  async getMembers(){
    try{
      return lastValueFrom(this.http.get('https://localhost:5001/api/member'));
    }catch(error){
      console.log(error);
      throw error;
    }
  }
}
