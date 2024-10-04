export interface Kmcm_person {
  kmcm_id?: number | null;
  kmcm_name: string;
  kmcm_lastname: string;
  kmcm_address?: string;
  kmcm_phone: string;
  kmcm_birthdate: string;
}

export interface Kmcm_user {
  kmcm_id?: number | null;
  kmcm_username: string;
  kmcm_password: string;
  kmcm_person_id?: number | null;
  kmcm_person?: Kmcm_person;
}


