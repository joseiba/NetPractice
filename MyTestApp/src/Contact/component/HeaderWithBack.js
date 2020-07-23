import React, { Component} from 'react';
import { Header, Left, Body, Right, Title, Button, Icon, Text} from 'native-base';

export default class HeaderWithBack extends Component{
    contructor(props){
        super(props);

        this.state = {
            title: props.title
        }
    }

    render(){
        return(
            <Header>
                <Left>
                    <Button transparent onPres={ () => this.props.navigation.goBack()}>
                        <Icon name='arrow-back'/>
                    </Button>
                </Left>
                <Body>
                    <Title>{this.state.title}</Title>
                </Body>
            </Header>
        )
    }
}