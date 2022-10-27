import React from "react";
import { useUser } from "../../authentication/use-auth";
import { Header, Container, Divider } from "semantic-ui-react";
import "./user-page.css";

export const UserPage = () => {
  const user = useUser();
  return (
    <div className="user-page-container">
      <div>
        <Header>User Information</Header>
        <Container textAlign="left">
          <Header size="small">Profile Color Name</Header>
          <p>{user.profileColorId}</p>
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
          <p>{user.birthDay}</p>
          <Divider />
        </Container>
      </div>
    </div>
  );
};
