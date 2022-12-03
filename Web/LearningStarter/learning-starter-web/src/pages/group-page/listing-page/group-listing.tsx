import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, GroupGetDto } from "../../../constants/types";
import { useHistory } from "react-router-dom";
import { routes } from "../../../routes/config";
import "./group-listing.css";

export const GroupListingPage = () => {
  const [groups, setGroup] = useState<GroupGetDto[]>();
  const history = useHistory();

  useEffect(() => {
    const fetchGroup = async () => {
      const response = await axios.get<ApiResponse<GroupGetDto[]>>(
        `${BaseUrl}/api/groups`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setGroup(response.data.data);
      }
    };

    fetchGroup();
  }, []);

  return (
    <>
      {groups && (
        <>
          <Header>Groups</Header>
          <Button
            type="button"
            onClick={() => history.push(routes.groups.create)}
          >
            + Create
          </Button>
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
                <Table.HeaderCell>Image</Table.HeaderCell>
                <Table.HeaderCell>Edit Group</Table.HeaderCell>
                <Table.HeaderCell>Delete Group</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {groups.map((group) => {
                return (
                  <Table.Row key={group.id}>
                    <Table.Cell>{group.id}</Table.Cell>
                    <Table.Cell>{group.name}</Table.Cell>
                    <Table.Cell>{group.image}</Table.Cell>
                    <Table.Cell>
                      <Button
                        positive
                        type="button"
                        content="Edit Group"
                        icon="pencil"
                        onClick={() =>
                          history.push(
                            routes.groups.update.replace(":id", `${group.id}`)
                          )
                        }
                      />
                    </Table.Cell>
                    <Table.Cell>
                      <Button
                        negative
                        type="button"
                        content="Delete Group"
                        icon="trash"
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
