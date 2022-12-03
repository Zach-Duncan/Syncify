import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useHistory, useRouteMatch } from "react-router-dom";
import { Button, Input } from "semantic-ui-react";
import { ApiResponse, GroupMemberGetDto } from "../../../constants/types";
import { routes } from "../../../routes/config";

export const GroupMembersDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>(); //make sure this is correct
  const id = match.params.id;
  const [groupMembers, setGroupMembers] = useState<GroupMemberGetDto>();

  useEffect(() => {
    const fetchGroupMembers = async () => {
      const response = await axios.get<ApiResponse<GroupMemberGetDto>>(
        `/api/group-members/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setGroupMembers(response.data.data);
    };

    fetchGroupMembers();
  }, [id]);

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<GroupMemberGetDto>>(
      `/api/group-members/${id}`
    );

    if (response.data.hasErrors) {
      toast.error("Error Occured, please try again", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.groupMembers.listing);
    }
  };

  return (
    <>
      {groupMembers && (
        <Formik initialValues={groupMembers} onSubmit={onSubmit}>
          <Form>
            <div className="group-members-delete-container">
              <label htmlFor="memberRoleId">MemberRoleId</label>
            </div>
            <div className="group-members-delete-container">
              <Field id="memberRoleId" name="memberRoleId">
                {({ field }) => <Input type="number" {...field} />}
              </Field>
            </div>
            <div className="group-members-delete-container">
              <label htmlFor="userId">UserId</label>
            </div>
            <div className="group-members-delete-container">
              <Field id="userId" name="userId">
                {({ field }) => <Input type="number" {...field} />}
              </Field>
            </div>
            <div className="group-members-delete-container">
              <label htmlFor="groupId">GroupId</label>
            </div>
            <div className="group-members-delete-container">
              <Field id="groupId" name="groupId">
                {({ field }) => <Input type="number" {...field} />}
              </Field>
            </div>
            <div className="group-members-delete-container">
              <Button type="submit">Delete</Button>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
