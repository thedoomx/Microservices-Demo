import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {
  @Input('queryString') queryString;
  // @Output('emitter') emitter = new EventEmitter<Array<Car>>();
  //constructor(private carService: CarsService) { }
  constructor() { }

  ngOnInit(): void {
  }

  changePage() {
    // this.carService.search(this.queryString).subscribe(res => {
    //   this.emitter.emit(res);
    // })
  }

}
