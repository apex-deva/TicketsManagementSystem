export interface Ticket {
     id: string;
  creationDateTime: Date;
  phoneNumber: string;
  governorate: string;
  city: string;
  district: string;
  status: string;
  colorCode: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface CreateTicketRequest {
  phoneNumber: string;
  governorate: string;
  city: string;
  district: string;
}