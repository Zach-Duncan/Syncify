import React from "react";
import { useUser } from "../../../authentication/use-auth";
import { Header, Container, Divider } from "semantic-ui-react";
import "./users-profle.css";

export const UsersProfilePage = () => {
  const user = useUser();
  const color = user.profileColor.colors;
  return (
    <div>
      <div color={color}>
        <Container textAlign="left">
          <Container textAlign="center">
            <Header size="huge">User Information</Header>
          </Container>

          <Header size="small">Profile Color Name</Header>
          <p>{user.profileColor.colors}</p>
          <Divider />
          <Header size="small">First Name</Header>
          <p>{user.firstName}</p>
          <Divider />
          <Header size="small">Last Name</Header>
          <p>{user.lastName}</p>
          <Divider />
          <Header size="small">User Name</Header>
          <p>{user.username}</p>
          <Divider />
          <Header size="small">Phone Number</Header>
          <p>{user.phoneNumber}</p>
          <Divider />
          <Header size="small">Email</Header>
          <p>{user.email}</p>
          <Divider />
          <Header size="small">Birthday</Header>
          <p>{user.birthday}</p>
          <Divider />
        </Container>
      </div>
    </div>
  );
};
