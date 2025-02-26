export interface Employee {
  id: string;
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  documentNumber: string;
  phoneNumbers: string[];
  managerId: string | null;
  managerName: string | null;
  dateOfBirth: string;
  role: string;
  hierarchicalLevel: number;
  createdAt: string | null;
  updatedAt: string;
  password?: string; 
}

export interface EmployeeFormProps {
    employee?: Employee; 
    onSave: (employee: Employee) => void;
  }