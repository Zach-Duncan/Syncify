import "../delete-page/todos-delete.css";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import { Button, Header, Input } from "semantic-ui-react";
import React, { useState, useEffect } from "react";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../../routes/config";
import { useHistory } from "react-router-dom";
import { ApiResponse, ToDoGetDto } from "../../../../constants/types";
import toast from "react-hot-toast";

export const ToDoDeletePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [todo, setToDos] = useState<ToDoGetDto>();

  useEffect(() => {
    const fetchToDos = async () => {
      const response = await axios.get<ApiResponse<ToDoGetDto>>(
        `/api/to-dos/${id}`
      );

      if (response.data.hasErrors) {
        console.log(response.data.errors);
        return;
      }

      setToDos(response.data.data);
    };

    fetchToDos();
  });

  const onSubmit = async () => {
    const response = await axios.delete<ApiResponse<ToDoGetDto>>(
      `/api/to-dos/${id}`
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.toDos.listing);
      toast.success("To-Do successfully deleted", {
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
      {todo && (
        <Formik initialValues={todo} onSubmit={onSubmit}>
          <Form>
            <div className="todos-delete-container">
              <Header>Delete To-Do</Header>
            </div>
            <div className="todos-delete-container">
              <label htmlFor="title">Title</label>
            </div>
            <div className="todos-delete-container">
              <Field id="title" name="title">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-delete-container">
              <label htmlFor="description">Description</label>
            </div>
            <div className="todos-delete-container">
              <Field id="description" name="description">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-delete-container">
              <label htmlFor="startDate">Start Date</label>
            </div>
            <div className="todos-delete-container">
              <Field id="startDate" name="startDate">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-delete-container">
              <label htmlFor="endDate">End Date</label>
            </div>
            <div className="todos-delete-container">
              <Field id="endDate" name="endDate">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-delete-container">
              <label htmlFor="calendar.group.name">Calendar</label>
            </div>
            <div className="todos-delete-container">
              <Field id="calendar.group.name" name="calendar.group.name">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-delete-container">
              <Button
                negative
                icon="trash"
                content="Delete"
                labelPosition="left"
                type="submit"
              />
            </div>
          </Form>
        </Formik>
      )}
    </>
  );
};
