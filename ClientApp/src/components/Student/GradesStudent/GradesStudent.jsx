import React from "react";
import { Container, Row, Col, Card, Form, Button, Nav } from "react-bootstrap";
import BootstrapTable from "react-bootstrap-table-next";
import cellEditFactory from "react-bootstrap-table2-editor";

const columns = [
  {
    dataField: "criteria",
    text: "Criteria",
    editable: false,
  },
  {
    dataField: "weight",
    text: "Weight",
    // Product Name column can't be edit anymore
    editable: false,
  },
  {
    dataField: "mark",
    text: "Mark",
    editable: false,
  },
  {
    dataField: "instructorComment",
    text: "Instructor Comment",
    editable: false,
  },
  {
    dataField: "studentComment",
    text: "Student Comment",
  },
];
const homework = [
  {
    criteria: "Use CSS",
    weight: 10,
    mark: 23,
    instructorComment: "good",
    studentComment: "dfedfdf",
  },
  {
    criteria: "Use React",
    weight: 10,
    mark: 23,
    instructorComment: "good",
    studentComment: "dfdfdf",
  },
  {
    criteria: "Validate Code",
    weight: 10,
    mark: 22,
    instructorComment: "good",
    studentComment: "dfdfdf",
  },
  {
    criteria: "Write HTML",
    weight: 10,
    mark: 21,
    instructorComment: "good",
    studentComment: "dfdfdf",
  },
];

const GradesStudent = () => {
  return (
    <React.Fragment>
      <Container>
        <Row>
          <Col xs={2}>
            <Nav defaultActiveKey="/home" className="flex-column">
              <Nav.Link href="/home">HW Name</Nav.Link>
              <Nav.Link eventKey="link-1">HW Name</Nav.Link>
              <Nav.Link eventKey="link-2">HW Name</Nav.Link>
              <Nav.Link eventKey="link-2">HW Name</Nav.Link>
              <Nav.Link eventKey="link-2">HW Name</Nav.Link>
            </Nav>
          </Col>
          <Col xs={10}>
            <BootstrapTable
              keyField="id"
              data={homework}
              columns={columns}
              cellEdit={cellEditFactory({
                mode: "click",
                blurToSave: true,
              })}
            />
            <Button className="float-right">Save</Button>
          </Col>
        </Row>
      </Container>
    </React.Fragment>
  );
};

export default GradesStudent;
