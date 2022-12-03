import axios from "axios";
import React, { useEffect, useState } from "react";
import { ApiResponse, ToDoGetDto } from "../../../../constants/types";
import { BaseUrl } from "../../../../constants/env-cars";
import { Button, Header, Table } from "semantic-ui-react";
import { useHistory } from "react-router-dom";
import { routes } from "../../../../routes/config";
import "./todos-listing.css";

export const ToDoListingPage = () => {
  const [todos, setToDos] = useState<ToDoGetDto[]>();
  const history = useHistory();
  const fetchToDos = async () => {
    const response = await axios.get<ApiResponse<ToDoGetDto[]>>(
      `${BaseUrl}/api/to-dos`
    );
    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      setToDos(response.data.data);
    }
  };

  useEffect(() => {
    fetchToDos();
  }, []);

  return (
    <>
      {todos && (
        <>
          <Header>To-Dos</Header>
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Title</Table.HeaderCell>
                <Table.HeaderCell>Description</Table.HeaderCell>
                <Table.HeaderCell>Start Date</Table.HeaderCell>
                <Table.HeaderCell>End Date</Table.HeaderCell>
                <Table.HeaderCell>Group Calendar</Table.HeaderCell>
                <Table.HeaderCell>Edit To-Do</Table.HeaderCell>
                <Table.HeaderCell>Delete To-Do</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {todos.map((todo) => {
                return (
                  <Table.Row key={todo.id}>
                    <Table.Cell>{todo.id}</Table.Cell>
                    <Table.Cell>{todo.title}</Table.Cell>
                    <Table.Cell>{todo.description}</Table.Cell>
                    <Table.Cell>{todo.startDate}</Table.Cell>
                    <Table.Cell>{todo.endDate}</Table.Cell>
                    <Table.Cell>{todo.calendar.group.name}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        labelPosition="left"
                        type="button"
                        content="Edit To-Do"
                        icon="pencil"
                        onClick={() =>
                          history.push(
                            routes.toDos.update.replace(":id", `${todo.id}`)
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        labelPosition="left"
                        type="button"
                        content="Delete To-Do"
                        icon="trash"
                        onClick={() =>
                          history.push(
                            routes.toDos.delete.replace(":id", `${todo.id}`)
                          )
                        }
                      />
                    </Table.Cell>
                  </Table.Row>
                );
              })}
            </Table.Body>
          </Table>
        </>
      )}
    </>
  );
};
