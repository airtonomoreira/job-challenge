
"use client"; 

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import EmployeeForm from "@/components/EmployeeForm";
import EmployeeList from "@/components/EmployeeList";
import { Employee } from "@/types/employee";
import { Button } from "@/components/ui/button";

export default function EmployeesPage() {
  const router = useRouter();
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState(true);
  const [isEditing, setIsEditing] = useState(false);
  const [isCreating, setIsCreating] = useState(false);
  const [currentEmployee, setCurrentEmployee] = useState<Employee | undefined>(
    undefined
  );

  
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      router.push("/auth/login"); 
    }
  }, [router]);

  
  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    const token = localStorage.getItem("token");
    if (!token) return;

    try {
      const response = await fetch("http://localhost:5179/api/Employees", {
        headers: {
          Authorization: `Bearer ${token}`, 
        },
      });

      if (response.ok) {
        const data = await response.json();
        setEmployees(data); 
      } else {
        console.error("Erro ao carregar funcionários");
      }
    } catch (err) {
      console.error("Erro na requisição:", err);
    } finally {
      setLoading(false); 
    }
  };

  const handleSave = async (employee: Employee) => {
    const token = localStorage.getItem("token");
    if (!token) return;

    try {
      const url = currentEmployee
        ? `http://localhost:5179/api/Employees/${currentEmployee.id}` 
        : "http://localhost:5179/api/Employees"; 

      const method = currentEmployee ? "PUT" : "POST";

      const response = await fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`, 
        },
        body: JSON.stringify(employee),
      });

      if (response.ok) {
        alert(
          currentEmployee
            ? "Funcionário atualizado com sucesso!"
            : "Funcionário criado com sucesso!"
        );
        fetchEmployees(); 
        cancelForm(); 
      } else {
        alert("Erro ao salvar funcionário");
      }
    } catch (err) {
      console.error("Erro na requisição:", err);
    }
  };

  const handleDelete = async (id: string) => {
    const token = localStorage.getItem("token");
    if (!token) return;

    const confirmDelete = window.confirm(
      "Tem certeza que deseja excluir este funcionário?"
    );
    if (!confirmDelete) return; 

    try {
      const response = await fetch(
        `http://localhost:5179/api/Employees/${id}`,
        {
          method: "DELETE",
          headers: {
            Authorization: `Bearer ${token}`, 
          },
        }
      );

      if (response.ok) {
        alert("Funcionário excluído com sucesso!");
        fetchEmployees(); 
      } else {
        alert("Erro ao excluir funcionário");
      }
    } catch (err) {
      console.error("Erro na requisição:", err);
    }
  };

  const handleEdit = (employee: Employee) => {
    setCurrentEmployee(employee); 
    setIsEditing(true); 
    setIsCreating(false); 
  };

  const handleCreate = () => {
    setCurrentEmployee(undefined); 
    setIsCreating(true); 
    setIsEditing(false); 
  };

  const cancelForm = () => {
    setCurrentEmployee(undefined); 
    setIsEditing(false); 
    setIsCreating(false); 
  };
  if (loading) {
    return <div>Carregando...</div>; 
  }

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Gerenciamento de Funcionários</h1>

      {/* Botão para novo funcionário */}
      {!isEditing && !isCreating && (
        <Button onClick={handleCreate} className="mb-4">
          Novo Funcionário
        </Button>
      )}

      {/* Formulário de edição/criação */}
      {(isEditing || isCreating) && (
        <div className="mb-4">
          <EmployeeForm
            employee={currentEmployee} 
            onSave={handleSave}
            onCancel={cancelForm} 
          />
        </div>
      )}

      {/* Lista de funcionários */}
      {!isEditing && !isCreating && (
        <EmployeeList
          employees={employees}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      )}
    </div>
  );
}
