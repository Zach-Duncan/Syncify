import "../update page/todos-update.css";
import axios from "axios";
import { Field, Form, Formik } from "formik";
import { Button, Dropdown, Input } from "semantic-ui-react";
import React, { useState, useEffect } from "react";
import { useRouteMatch } from "react-router-dom";
import { routes } from "../../../../routes/config";
import { useHistory } from "react-router-dom";
import {
  ApiResponse,
  OptionDto,
  ToDoGetDto,
  ToDoUpdateDto,
} from "../../../../constants/types";
import toast from "react-hot-toast";

export const ToDoUpdatePage = () => {
  const history = useHistory();
  let match = useRouteMatch<{ id: string }>();
  const id = match.params.id;
  const [todo, setToDos] = useState<ToDoGetDto>();
  const [calendarOptions, setCalendarOptions] = useState<OptionDto[]>();
  console.log("debug", calendarOptions);

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
  }, [id]);

  useEffect(() => {
    async function getCalendarOptions() {
      const response = await axios.get<ApiResponse<OptionDto[]>>(
        "/api/calendars/options"
      );

      setCalendarOptions(response.data.data);
    }

    getCalendarOptions();
  }, []);

  const onSubmit = async (values: ToDoUpdateDto) => {
    const response = await axios.put<ApiResponse<ToDoGetDto>>(
      `/api/to-dos/${id}`,
      values
    );

    if (response.data.hasErrors) {
      response.data.errors.forEach((err) => {
        console.log(err.message);
      });
    } else {
      history.push(routes.toDos.listing);
      toast.success("To-Do successfully updated", {
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
            <div className="todos-update-container">
              <label htmlFor="title">Title</label>
            </div>
            <div className="todos-update-container">
              <Field id="title" name="title">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-update-container">
              <label htmlFor="description">Description</label>
            </div>
            <div className="todos-update-container">
              <Field id="description" name="description">
                {({ field }) => <Input {...field} />}
              </Field>
            </div>
            <div className="todos-update-container">
              <label htmlFor="startDate">Start Date</label>
            </div>
            <div className="todos-update-container">
              <Field id="startDate" name="startDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
            </div>
            <div className="todos-update-container">
              <label htmlFor="endDate">End Date</label>
            </div>
            <div className="todos-update-container">
              <Field id="endDate" name="endDate">
                {({ field }) => <Input type="date" {...field} />}
              </Field>
            </div>
            <div className="todos-update-container">
              <label htmlFor="calendar">Calendar</label>
            </div>
            <div className="todos-update-container">
              <Field name="calendarId" id="calendarId" className="field">
                {({ field, form }) => (
                  <Dropdown
                    selection
                    options={calendarOptions}
                    {...field}
                    onChange={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                    onBlur={(_, { name, value }) =>
                      form.setFieldValue(name, value)
                    }
                  />
                )}
              </Field>
            </div>
            <div className="todos-update-container">
              <Button
                positive
                icon="pencil"
                content="Update"
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
