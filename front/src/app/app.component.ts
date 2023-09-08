import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RankingComponent } from './components/ranking/ranking.component';
import { AppService } from './services/app.service';
import { FormComponent } from './components/form/form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RankingComponent, FormComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'front';

  constructor(private appService: AppService) {
    this.appService.init();
  }
}
