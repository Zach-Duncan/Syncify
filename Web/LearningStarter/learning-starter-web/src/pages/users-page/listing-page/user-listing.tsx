import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, UserGetDto } from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import "./user-listing.css";

export const UsersListingPage = () => {
  const [users, setUsers] = useState<UserGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchUsers = async () => {
      const response = await axios.get<ApiResponse<UserGetDto[]>>(
        `${BaseUrl}/api/users`
      );
      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setUsers(response.data.data);
      }
    };

    fetchUsers();
  }, []);

  return (
    <>
      {users && (
        <>
          <Header>Users</Header>
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Profile Color</Table.HeaderCell>
                <Table.HeaderCell>First Name</Table.HeaderCell>
                <Table.HeaderCell>Last Name</Table.HeaderCell>
                <Table.HeaderCell>Username</Table.HeaderCell>
                <Table.HeaderCell>Phone Number</Table.HeaderCell>
                <Table.HeaderCell>Email</Table.HeaderCell>
                <Table.HeaderCell>Birthday</Table.HeaderCell>
                <Table.HeaderCell>Edit User</Table.HeaderCell>
                <Table.HeaderCell>Delete User</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {users.map((users) => {
                return (
                  <Table.Row key={users.id}>
                    <Table.Cell>{users.id}</Table.Cell>
                    <Table.Cell>{users.profileColor.colors}</Table.Cell>
                    <Table.Cell>{users.firstName}</Table.Cell>
                    <Table.Cell>{users.lastName}</Table.Cell>
                    <Table.Cell>{users.username}</Table.Cell>
                    <Table.Cell>{users.phoneNumber}</Table.Cell>
                    <Table.Cell>{users.email}</Table.Cell>
                    <Table.Cell>{users.birthday}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        content="Edit User"
                        icon="pencil"
                        onClick={() =>
                          history.push(
                            routes.users.update.replace(":id", `${users.id}`)
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        content="Delete User"
                        icon="trash"
                        onClick={() =>
                          history.push(
                            routes.users.delete.replace(":id", `${users.id}`)
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
