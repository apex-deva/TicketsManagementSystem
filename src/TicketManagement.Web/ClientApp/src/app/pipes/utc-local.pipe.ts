import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  standalone: true,
  name: 'utcToLocal'
})
export class UtcToLocalPipe implements PipeTransform {
  transform(utcDate: string | Date, format: string = 'short'): string {
    if (!utcDate) return '';

    // Ensure we have a Date object
    const date = new Date(utcDate);

    // Check if date is valid
    if (isNaN(date.getTime())) {
      console.warn('Invalid date received:', utcDate);
      return 'Invalid date';
    }

    // Get local time offset in minutes and convert to milliseconds
    const offset = date.getTimezoneOffset() * 60000;

    // Apply the offset to get local time
    const localDate = new Date(date.getTime() - offset);

    // Format using Angular DatePipe
    return new DatePipe('en-US').transform(localDate, format) || '';
  }
}
