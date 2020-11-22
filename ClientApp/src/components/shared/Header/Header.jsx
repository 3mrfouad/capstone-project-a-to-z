import React, { useState } from "react";
import {
  Navbar,
  Nav,
  NavDropdown,
  Form,
  FormControl,
  Button,
  Container,
} from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
// import { useDispatch, useSelector } from "react-redux";

const Header = () => {
//   const userLogin = useSelector((state) => state.userLogin);
//   const { userInfo } = userLogin;
//   const dispatch = useDispatch();
//   const logoutHandler = () => {
//     dispatch(logout());
//   };
  return (
    <React.Fragment>
      <Navbar bg="light" expand="lg" collapseOnSelect>
        <Container>
          <LinkContainer to="/">
            <Navbar.Brand>XXXXX SYSTEM</Navbar.Brand>
          </LinkContainer>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="ml-auto">
              <LinkContainer to="/">
                <Nav.Link>
                  {" "}
                  <i className="fas fa-shopping-cart"></i> <strong>Hello</strong>{" "}
                </Nav.Link>
              </LinkContainer>
              {/* {userInfo ? (
                <NavDropdown title={userInfo.name} id="username">
                  <LinkContainer to="/profile">
                    <NavDropdown.Item>Profile</NavDropdown.Item>
                  </LinkContainer>
                  <NavDropdown.Item onClick={logoutHandler}>
                    Logout
                  </NavDropdown.Item>
                </NavDropdown>
              ) : (
                <LinkContainer to="/login">
                  <Nav.Link>
                    {" "}
                    <i className="fas fa-user"></i> <strong>Sign In</strong>{" "}
                  </Nav.Link>
                </LinkContainer>
              )} */}
              {/* {
                userInfo && userInfo.isAdmin && (
                  <NavDropdown title='Admin' id="adminmenu">
                  <LinkContainer to="/admin/userlist">
                    <NavDropdown.Item>Users</NavDropdown.Item>
                  </LinkContainer>
                  <LinkContainer to="/admin/productlist">
                    <NavDropdown.Item>Products</NavDropdown.Item>
                  </LinkContainer>
                  <LinkContainer to="/admin/orderlist">
                    <NavDropdown.Item>Orders</NavDropdown.Item>
                  </LinkContainer>
                </NavDropdown>
                )
              } */}
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
      </React.Fragment>
  );
};

export default Header;
