import axios from "axios";
import { Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useHistory, useRouteMatch } from "react-router-dom";
import { Button, Input } from "semantic-ui-react";
import {
  ApiResponse,
  GroupMemberGetDto,
  GroupMemberUpdateDto,
} from "../../../constants/types";
import { routes } from "../../../routes/config";

export const GroupMembersUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>(); //check to see if this is really needed
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

  const onSubmit = async (values: GroupMemberUpdateDto) => {
    const response = await axios.put<ApiResponse<GroupMemberGetDto>>(
      `/api/group-members/${id}`,
      values
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
      toast.success("Group Member successfully updated", {
        position: "top-center",
        style: {
          background: "#333",
          color: "#fff",
        },
      });
    }
  };

  return (
    <>
      {groupMembers && (
        <Formik initialValues={groupMembers} onSubmit={onSubmit}>
          <Form>
            <div>
              <label htmlFor="memberRoleId">MemberRoleId</label>
            </div>
            <Field id="memberRoleId" name="memberRoleId">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
            <div>
              <label htmlFor="userId">UserId</label>
            </div>
            <Field id="userId" name="userId">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
            <div>
              <label htmlFor="groupId">GroupId</label>
            </div>
            <Field id="groupId" name="groupId">
              {({ field }) => <Input type="number" {...field} />}
            </Field>
            <div>
              <Button type="submit">Update</Button>
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
