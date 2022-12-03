import axios from "axios";
import React, { useEffect, useState } from "react";
import { Header, Table } from "semantic-ui-react";
import { BaseUrl } from "../../../constants/env-cars";
import { ApiResponse, MemberRoleGetDto } from "../../../constants/types";
import "./member-role-listing.css";

export const MemberRoleListingPage = () => {
  const [memberRoles, setMemberRoles] = useState<MemberRoleGetDto[]>();

  useEffect(() => {
    const fetchMemberRoles = async () => {
      const response = await axios.get<ApiResponse<MemberRoleGetDto[]>>(
        `${BaseUrl}/api/member-roles`
      );

      if (response.data.hasErrors) {
        response.data.errors.forEach((err) => {
          console.log(err.message);
        });
      } else {
        setMemberRoles(response.data.data);
      }
    };

    fetchMemberRoles();
  }, []);

  return (
    <>
      {memberRoles && (
        <>
          <Header>Member Roles</Header>
          <Table striped celled>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>Name</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {memberRoles.map((memberRole) => {
                return (
                  <Table.Row key={memberRole.id}>
                    <Table.Cell>{memberRole.id}</Table.Cell>
                    <Table.Cell>{memberRole.name}</Table.Cell>
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
