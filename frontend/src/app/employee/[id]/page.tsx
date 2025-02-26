"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import EmployeeForm from "@/components/EmployeeForm";
import { Employee } from "@/types/employee";

export default function EditEmployeePage({
  params,
}: {
  params: { id: string };
}) {
  const router = useRouter();
  const [employee, setEmployee] = useState<Employee | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchEmployee = async () => {
      const token = localStorage.getItem("token");
      if (!token) {
        router.push("/auth/login");
        return;
      }

      try {
        const response = await fetch(
          `http://localhost:5179/api/Employees/${params.id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (response.ok) {
          const data = await response.json();
          console.log("Dados do funcionário carregados:", data);
          setEmployee(data);
        } else {
          console.error("Erro ao carregar funcionário");
          router.push("/employees");
        }
      } catch (err) {
        console.error("Erro na requisição:", err);
        router.push("/employees");
      } finally {
        setLoading(false);
      }
    };

    fetchEmployee();
  }, [params.id, router]);

  const handleSave = async (updatedEmployee: Employee) => {
    const token = localStorage.getItem("token");
    if (!token) {
      alert("Token de autenticação não encontrado. Faça login novamente.");
      router.push("/auth/login");
      return;
    }

    try {
      
      console.log("Corpo da requisição:", {
        firstName: updatedEmployee.firstName,
        lastName: updatedEmployee.lastName,
        email: updatedEmployee.email,
        documentNumber: updatedEmployee.documentNumber,
        phoneNumbers: updatedEmployee.phoneNumbers,
        managerId: updatedEmployee.managerId,
        managerName: updatedEmployee.managerName,
        dateOfBirth: updatedEmployee.dateOfBirth,
        role: updatedEmployee.role,
        hierarchicalLevel: updatedEmployee.hierarchicalLevel,
        password: updatedEmployee.password,
      });

      const response = await fetch(
        `http://localhost:5179/api/Employees/${updatedEmployee.id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`, 
          },
          body: JSON.stringify({
            firstName: updatedEmployee.firstName,
            lastName: updatedEmployee.lastName,
            email: updatedEmployee.email,
            documentNumber: updatedEmployee.documentNumber,
            phoneNumbers: updatedEmployee.phoneNumbers,
            managerId: updatedEmployee.managerId,
            managerName: updatedEmployee.managerName,
            dateOfBirth: updatedEmployee.dateOfBirth,
            role: updatedEmployee.role,
            hierarchicalLevel: updatedEmployee.hierarchicalLevel,
            password: updatedEmployee.password,
          }),
        }
      );

      if (response.ok) {
        alert("Funcionário atualizado com sucesso!");
        router.push("/employees"); 
      } else {
        const errorData = await response.json(); 
        console.error("Erro ao atualizar funcionário:", errorData); 

        if (errorData.errors) {
          console.error("Erros de validação:", errorData.errors);
          alert(`Erros de validação: ${JSON.stringify(errorData.errors)}`);
        } else {
          alert(`Erro ao atualizar funcionário: ${errorData.title}`);
        }
      }
    } catch (err) {
      console.error("Erro na requisição:", err); 
      alert("Erro ao atualizar funcionário");
    }
  };

  const handleCancel = () => {
    router.push("/employee"); 
  };

  if (loading) {
    return <div>Carregando...</div>; 
  }

  if (!employee) {
    return <div>Funcionário não encontrado.</div>; 
  }

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Editar Funcionário</h1>
      <EmployeeForm
        employee={employee}
        onSave={handleSave}
        onCancel={handleCancel}
      />
    </div>
  );
}
